using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq; 
using System.Windows.Forms; 

namespace QiPOS
{
    public class GridPrinter
    {
        private DataGridView grid;
        public DataGridView MagazineGrid { get; set; }

        private string title;
        public string Line1 { get; set; }
        public string Line2 { get; set; }

        public GridPrinter(DataGridView sourceGrid, string reportTitle)
        {
            grid = sourceGrid;
            title = reportTitle;
        }

        public void Print()
        {
            PrintDocument doc = new PrintDocument();
            //doc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            doc.PrinterSettings.PrintToFile = true;
            doc.PrinterSettings.PrintFileName = @"C:\Temp\result.pdf";            
            doc.DefaultPageSettings.Landscape = true;

            doc.PrintPage += PrintPage;

            PrintDialog dlg = new PrintDialog();
            dlg.Document = doc;

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                doc.PrinterSettings = dlg.PrinterSettings;
                doc.Print();
            }
        }

        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font font = new Font("Arial", 9);
            Font headerFont = new Font("Arial", 11, FontStyle.Bold);
            Font centerFont = new Font("Arial", 10, FontStyle.Regular);

            int startX = 40;
            int y = 50;
            int rowHeight = 25;
            int cellPadding = 5;

            // Title
            g.DrawString(title, headerFont, Brushes.Black, startX, y);
            y += 30;

            // Centered Line1 and Line2
            int pageWidth = e.PageBounds.Width;
            if (!string.IsNullOrEmpty(Line1))
            {
                SizeF size1 = g.MeasureString(Line1, centerFont);
                g.DrawString(Line1, centerFont, Brushes.Black, (pageWidth - size1.Width) / 2, y);
                y += 20;
            }

            if (!string.IsNullOrEmpty(Line2))
            {
                SizeF size2 = g.MeasureString(Line2, centerFont);
                g.DrawString(Line2, centerFont, Brushes.Black, (pageWidth - size2.Width) / 2, y);
                y += 30;
            }

            // Prepare columns
            var visibleCols = grid.Columns.Cast<DataGridViewColumn>()
                                  .Where(c => c.Visible)
                                  .OrderBy(c => c.DisplayIndex)
                                  .ToList();

            List<int> colWidths = visibleCols.Select((col, index) =>
            {
                if (index == 0)
                    return 100; // set first column to fixed 100
                return Math.Max(60, TextRenderer.MeasureText(col.HeaderText, font).Width);
            }).ToList();


            int x = startX;
            y += rowHeight;

            decimal total = 0;

            // Data rows
            foreach (DataGridViewRow row in grid.Rows)
            {
                if (row.IsNewRow) continue;

                var firstCell = row.Cells.Cast<DataGridViewCell>()
                                   .FirstOrDefault(c => c.Visible && c.Value != null);

                string firstText = firstCell?.Value?.ToString() ?? "";

                x = startX;
                //int? qty = null;
                //int? rt = null;
                decimal? price = null;
                if (visibleCols.Count > 1)
                {
                    var priceCell = row.Cells[visibleCols[1].Index];
                    if (decimal.TryParse(priceCell.Value?.ToString(), out decimal p))
                        price = p;
                }
                int count = 0;
                int rets= 0;

                for (int colIndex = 0; colIndex < visibleCols.Count; colIndex++)
                {
                    var col = visibleCols[colIndex];
                    var cell = row.Cells[col.Index];
                    string text = cell.Value?.ToString() ?? "";

                    g.DrawString(text, font, Brushes.Black, x + cellPadding, y);

                    string header = col.HeaderText.Trim().ToUpper();
                    if (header.Contains("SUPPLY") && int.TryParse(text, out int q))
                        count += q;
                    else if (header.Contains("RETURN") && int.TryParse(text, out int r))
                        rets += r;

                    x += colWidths[colIndex];
                }

                // Now calculate (QTY - RT) * PRICE
                if (price.HasValue)
                {
                    int netQty = count - rets;
                    decimal rowTotal = netQty * price.Value;
                    total += rowTotal;
                }
                y += rowHeight;
            }

            if (MagazineGrid != null && MagazineGrid.Rows.Count > 0)
            {
                y += 40; // space from previous section
                Font sectionFont = new Font("Arial", 10, FontStyle.Bold);
                g.DrawString("Magazine List for " + Line1, sectionFont, Brushes.Blue, startX, y);
                y += 30;

                var magCols = MagazineGrid.Columns.Cast<DataGridViewColumn>()
                                .Where(c => c.Visible)
                                .OrderBy(c => c.DisplayIndex)
                                .ToList();

                List<int> magColWidths = magCols.Select((col, index) =>
                {
                    string name = col.HeaderText.Trim().ToUpper();
                    if (name == "QTY" || name == "RT")
                        return 45;
                    if (index == 0) return 240; // widen first column
                    return Math.Max(60, TextRenderer.MeasureText(col.HeaderText, font).Width);
                }).ToList();


                x = startX;
                for (int i = 0; i < magCols.Count; i++)
                {
                    g.DrawString(magCols[i].HeaderText, font, Brushes.Black, x, y);
                    x += magColWidths[i];
                }

                y += rowHeight;

                // Magazine rows
                foreach (DataGridViewRow row in MagazineGrid.Rows)
                {
                    if (row.IsNewRow) continue;

                    x = startX;

                    int qty = 0;
                    int rt = 0;
                    decimal price = 0;

                    for (int i = 0; i < magCols.Count; i++)
                    {
                        var col = magCols[i];
                        var cell = row.Cells[col.Index];
                        string text = "";

                        if (col.HeaderText.ToUpper().Contains("DATE"))
                        {
                            if (DateTime.TryParse(cell.Value?.ToString(), out DateTime dateVal))
                                text = dateVal.ToString("dd/MM");
                        }
                        else
                        {
                            text = cell.Value?.ToString() ?? "";
                        }

                        g.DrawString(text, font, Brushes.Black, x + cellPadding, y);

                        // capture for total
                        string header = col.HeaderText.Trim().ToUpper();
                        if (header == "QTY" && int.TryParse(text, out int q))
                            qty = q;
                        else if (header == "RT" && int.TryParse(text, out int r))
                            rt = r;
                        else if (header == "PRICE" && decimal.TryParse(text, out decimal p))
                            price = p;

                        x += magColWidths[i];
                    }

                    int net = qty - rt;
                    decimal rowTotal = net * price;

                    // print Net:
                    g.DrawString("Net: " + net.ToString(), font, Brushes.Black, startX + magColWidths.Sum() + 10, y);

                    // add to grand total
                    total += rowTotal;

                    y += rowHeight;
                }

            }

            // Bottom line
            y += 10;
            g.DrawLine(Pens.Black, startX, y, startX + colWidths.Sum(), y);
            y += 10;

            // Totals
            g.DrawString("Total:", font, Brushes.Black, startX, y);
            g.DrawString(total.ToString("C"), font, Brushes.Black, startX + colWidths.Sum() - 100, y);
            y += rowHeight;

            decimal gst = total / 11;
            g.DrawString("GST:", font, Brushes.Black, startX, y);
            g.DrawString(gst.ToString("C"), font, Brushes.Black, startX + colWidths.Sum() - 100, y);
        }

    }

}

