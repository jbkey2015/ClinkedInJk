using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinkedIn.Models;

namespace ClinkedIn.DataAccess
{
    public class ClinkerRepository
    {
        static List<Clinker> _clinkers = new List<Clinker> {
            new Clinker {
                Id = 1,
                Name = "Nathan",
                Age = 33,
                LockupReason = "Stealing TVs",
                Interests = new List<string>{ "Coding", "Stealing TVs"},
                Services = new List<Service>{ new Service { Title = "Shive Maker", Cost = "bar of soap"} },
                Sentence = new DateTime(2021,12,28)
            }
        };

        public void AddClinkerService(int id, Service service)
        {
            _clinkers[id - 1].Services.Add(service);
            // var clinkerToUpdate = _clinkers.(s => s.Id == id);

        }

        public void AddClinker(Clinker clinker)
        {
            clinker.Id = _clinkers.Max(x => x.Id) + 1;
            _clinkers.Add(clinker);
        }

        public Clinker GetById(int id)
        {
            return _clinkers.FirstOrDefault(c => c.Id == id);
        }

        public List<Clinker> GetClinkersByInterest(string interest)
        {
            var matchingClinkers = _clinkers.Where(x => x.Interests.Contains(interest));
            return matchingClinkers.ToList();
        }

        public void AddClinkerFriend(int id, Clinker friendToAdd)
        {
            _clinkers[id - 1].Friends.Add(friendToAdd);
        }

        public void AddClinkerEnemy(int id, Clinker enemyToAdd)
        {
            _clinkers[id - 1].Enemies.Add(enemyToAdd);
        }

        public string SentenceCountdown(int id)
        {
            var clinkerSentenced = GetById(id);
            DateTime daysLeft = clinkerSentenced.Sentence;
            DateTime startDate = DateTime.Now;

            TimeSpan sentenceLeft = daysLeft - startDate;        
            return string.Format($"{clinkerSentenced.Name} has {sentenceLeft.Days} Days Left In Sentence");
        }
    }
}
