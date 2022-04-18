using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jigsaw.Models
{
    public class Game
    {
        public long Id { get; set; }

        internal string _answer;
        internal string _currentState;

        [NotMapped]
        public List<int> Answer
        {
            get { return _answer == null ? null : JsonConvert.DeserializeObject<List<int>>(_answer); }
            set { _answer = JsonConvert.SerializeObject(value); }

        }

        [NotMapped]
        public List<int> CurrentState
        {
            get { return _currentState == null ? null : JsonConvert.DeserializeObject<List<int>>(_currentState); }
            set { _currentState = JsonConvert.SerializeObject(value); }

        }
    }
}
