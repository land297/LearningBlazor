using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning.Client.Services {
    public interface IMenuContentService {
        List<string> GetMenues();
    }

    public class MenuContentService : IMenuContentService {
        public List<string> GetMenues() {
            return new List<string>()
            {
                "hej","apa","grid"
            };
        }
    }
}
