using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace BERlogic.CallCenter.Models.Repositories
{
    public class FileReportJournalRepository : IFileReportJournal
    {
        private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "data", "filereport_journal.json");

        public FileReportJournalRepository()
        {
            if (!File.Exists(_path))
            {
                File.Create(_path);
            }
        }

        public IQueryable<FullReportJournal> Journals
        {
            get
            {
                using (var reader = new StreamReader(_path))
                using (JsonReader json = new JsonTextReader(reader))
                {
                    var list = new JsonSerializer().Deserialize<List<FullReportJournal>>(json);
                    if (list != null)
                    {
                        return list.OrderByDescending(o => o.RegistrationEventDate).AsQueryable();
                    }
                    return (new List<FullReportJournal>()).AsQueryable();
                }
            }
        }

        public async void Save(FullReportJournal journal)
        {
            using (var writer = new StreamWriter(_path))
            using (JsonWriter json = new JsonTextWriter(writer))
            {
                var obj = JsonConvert.SerializeObject(journal, Formatting.Indented);
                new JsonSerializer().Serialize(json, obj);
                await json.FlushAsync().ConfigureAwait(false);
            }
        }
    }
}
