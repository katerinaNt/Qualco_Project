using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QualcoOne.API.Services
{
    public class TempImportFile
    {
        Console.WriteLine("Hello World!");
            var context = new PlutoContext();

        //LINQ Syntax
        var TempImportedFiles = context.TempImportedFiles;

            //PRINT
            foreach (var TempImportedFile in TempImportedFiles)
                Console.WriteLine(TempImportedFile.Amount);

            Console.ReadLine();
    }
}
