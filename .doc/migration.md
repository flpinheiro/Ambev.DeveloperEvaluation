# Migration

To do the migration use the following cli code

```cli
Add-Migration -Name <string> -Context "DefaultContext" -Project "Ambev.DeveloperEvaluation.ORM" -StartupProject "Ambev.DeveloperEvaluation.WebApi"
```

to do the update of the data base use the follow cli code

```cli
Update-Database -Context "DefaultContext" -Project "Ambev.DeveloperEvaluation.ORM" -StartupProject "Ambev.DeveloperEvaluation.WebApi"
 ```