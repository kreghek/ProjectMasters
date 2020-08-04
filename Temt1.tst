${
    Template(Settings settings)
    {
        settings
            .IncludeCurrentProject()
            .OutputFilenameFactory = file =>
            {
                 return "ClientApp/src/app/models/" + file.Name.ToLowerInvariant().Replace(".cs", ".ts");
            };
    }
}

$Classes(ProjectMasters.Models.*)[
    export class $Name {  
        constructor($Properties[public $name: $Type][, ]) {  
        }  
    }  
]