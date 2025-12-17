# QiPos

## Database

The database server must create a new schema for this project. The schema should be named `qipos` since the database string is pointing to this by default. Within the schema, the tables are named as follows:

| Column | Name | Type | Length | Nullable | Default | Permissions |
| --- | --- | --- | --- | --- | --- | --- |
| Item Name | item_name | varchar | 100 | YES | NULL | select,update |
| Category | category | varchar | 50 | YES | NULL | select,update |
| Bar Code | bar_code | varchar | 15 | YES | NULL | select,update |
| Stock | stock | int | 11 | YES | NULL | select,update |
| Reorder Quantity | reorder_quant | int | 11 | YES | NULL | select,update |

## Committing your changes

1. Verify the working tree state with `git status` to confirm which files have been modified.
2. Stage the intended files using `git add <path>`.
3. Record the change with a meaningful message, for example `git commit -m "Describe the update"`.
4. Push the branch to the remote with `git push` if you maintain a remote repository.
5. Create a pull request using your hosting provider's workflow so the changes can be reviewed and merged.
