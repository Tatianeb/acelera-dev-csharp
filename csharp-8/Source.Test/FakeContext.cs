using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Codenation.Challenge.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Codenation.Challenge
{
    /// Fake Data
    /// powered by https://mockaroo.com/
    ///
    public class FakeContext
    {
        public DbContextOptions<CodenationContext> FakeOptions { get; }

        private Dictionary<Type, string> DataFileNames { get; } = 
            new Dictionary<Type, string>();
        private string FileName<T>() { return DataFileNames[typeof(T)]; }

        public FakeContext(string testName)
        {
            FakeOptions = new DbContextOptionsBuilder<CodenationContext>()
                .UseInMemoryDatabase(databaseName: $"Codenation_{testName}")
                .Options;
            
            DataFileNames.Add(typeof(User), Path.Combine("FakeData", "users.json"));
            DataFileNames.Add(typeof(Company),  Path.Combine("FakeData", "companies.json"));
            DataFileNames.Add(typeof(Models.Challenge),  Path.Combine("FakeData", "companies.json"));
            DataFileNames.Add(typeof(Acceleration),  Path.Combine("FakeData", "accelerations.json"));
            DataFileNames.Add(typeof(Submission), Path.Combine("FakeData", "submissions.json"));
            DataFileNames.Add(typeof(Candidate),  Path.Combine("FakeData", "candidates.json"));

        }

        public void FillWithAll()
        {
            FillWith<User>();
            FillWith<Company>();
            FillWith<Models.Challenge>();
            FillWith<Acceleration>();
            FillWith<Submission>();
            FillWith<Candidate>();
        }

        public void FillWith<T>() where T: class
        {
            using (var context = new CodenationContext(FakeOptions))
            {
                if (context.Set<T>().Count() == 0)
                {
                    foreach (T item in GetFakeData<T>())
                        context.Set<T>().Add(item);
                    context.SaveChanges();
                }
            }
        }

        public List<T> GetFakeData<T>()
        {
            string content = File.ReadAllText(FileName<T>());
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

    }
}
