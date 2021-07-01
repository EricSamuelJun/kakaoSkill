using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kakaoSkill {
    public class InputData {

    }
    public class Bot : Block {
        public Bot(string a, string b) : base(a, b) {

        }
    }
    public class Intent {
        public string id;
        public string name;
        public IntentExtra extra;
    }
    public class IntentExtra {
        public class reason {
            public int code;
            public string message;
            public reason(int c, string m) { code = c; message = m; }
        }
        public reason mreason;
        public string responseType;
        public List<Knowledge> knowledges;
        public IntentExtra(int code = -1, string message = "OK",string mrestype = "",List<Knowledge> knowledges = null) {
            if (code == -1)
                return;
            mreason = new reason(code,message);
            responseType = mrestype;
            this.knowledges = knowledges;
        }
    }
    
    public class Knowledge {
        public string answer;
        public string question;
        public List<string> categories;
        public string landingUrl;
        public string imageUrl;
    }
    public class Block {
        public string id;
        public string name;
        public Block(string mid, string mname) {
            id = mid;
            name = mname;
        }
    }
    public class UserRequest {
        string timeZone;
        Block block;
        string utterance;
        string lang;
        User user;
        Dictionary<string, string> p;
    }
    public class User {
        string id;
        string type;
        Dictionary<string, string> properties;
        public User(string mid, string mtype, Dictionary<string,string> mp = null) {
            id = mid;
            type = mtype;
            if (mp != null)
                properties = mp;
        }
    }
}
