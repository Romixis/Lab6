using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using System;
using System.Collections.Generic;
using System.IO;

namespace lab6
{
    class Sheets
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "MNK";
        public string SpreadsheetId { get; set; }
        static SheetsService service;
        public Sheets(string spreadSheetId)
        {
            SpreadsheetId = spreadSheetId;
            GoogleCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
            }
            service = new SheetsService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName
            });
        }
        public string[] GetSheetTitles()
        {
            List<string> titles = new List<string>();
            var sheets = service.Spreadsheets.Get(SpreadsheetId).Execute().Sheets;
            foreach (var item in sheets)
            {
                titles.Add(item.Properties.Title);
            }
            return titles.ToArray();
        }
        public object ReadEntries(string sheet)
        {
            var request = service.Spreadsheets.Values.Get(SpreadsheetId, sheet);
            var response = request.Execute();
            var values = response.Values;
            return values;
        }
        public void ExportToSheet(string sheet, double[,] data)
        {
            var range = $"{sheet}!A:F";
            var valueRange = new ValueRange
            {
                Values = new List<IList<object>>()
            };
            for (int i = 0; i < data.GetLength(0); i++)
            {
                List<object> oblist = new List<object>();
                for (int j = 0; j < data.GetLength(1); j++)
                {
                    oblist.Add(data[i, j]);
                }
                valueRange.Values.Add(oblist);
            }
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
        }
        public void AddNewSheet(string Name)
        {
            string sheetName = string.Format("{0} {1}", DateTime.Now.Month, DateTime.Now.Day);
            var addSheetRequest = new AddSheetRequest
            {
                Properties = new SheetProperties
                {
                    Title = Name
                }
            };
            BatchUpdateSpreadsheetRequest batchUpdateSpreadsheetRequest = new BatchUpdateSpreadsheetRequest
            {
                Requests = new List<Request>
                {
                    new Request
                    {
                        AddSheet = addSheetRequest
                    }
                }
            };

            var batchUpdateRequest = service.Spreadsheets.BatchUpdate(batchUpdateSpreadsheetRequest, SpreadsheetId);

            batchUpdateRequest.Execute();
        }
    }
}
