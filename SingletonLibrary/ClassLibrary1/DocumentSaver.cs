using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary1
{
    public class DocumentSaver
    {
        private static DocumentSaver instance;
        private string filePath;

        private DocumentSaver()
        {
            filePath = "document.txt";
        }

        public static DocumentSaver getInstance()
        {
            if (instance == null)
            {
                instance = new DocumentSaver();
            }

            return instance;
        }

        public void SaveDocument(Document document)
        {
            string text =
                $"Назва документа: {document.Name}\n" +
                $"Дата створення: {document.Date}\n\n" +
                document.Content;

            File.WriteAllText(filePath, text);
        }
    }
}
