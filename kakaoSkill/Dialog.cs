using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace kakaoSkill {
    public class Dialog {
        JObject payload;
        public Dialog(JObject mobj) {
            payload = mobj;
        }
        public string Utterance { get { return payload["userRequest"]["utterance"].ToString(); } private set { } }
    }
    public abstract class Response {
        protected Dictionary<string, object> parametors;
        public abstract Dictionary<string,object> get();
        public abstract void AddParams(KeyValuePair<string,object> m);
    }
    public class SimpleTextResponse : Response {
        public string text;
        public SimpleTextResponse(string mtext) {
            text = mtext;
            parametors = new Dictionary<string, object>();
        }
        public override Dictionary<string, object> get() {
            Dictionary<string, object> response = new Dictionary<string, object>();
            Dictionary<string, object> text = new Dictionary<string, object>();
            Dictionary<string, object> simpleText = new Dictionary<string, object>();
            Dictionary<string, object> data = new Dictionary<string, object>();
            List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
            Dictionary<string, object> template = new Dictionary<string, object>();
            text.Add("text", text);
            simpleText.Add("simpleText", text);
            outputs.Add(simpleText);
            foreach(KeyValuePair<string,object> m in parametors) {
                data.Add(m.Key, m.Value);
            }    
            template.Add("outputs", outputs);
            response.Add("version", "2.0");
            response.Add("template", template);
            response.Add("data",data);
            return response;
        }
        public override void AddParams(KeyValuePair<string, object> m) {
            KeyValuePair<string, object> p = m;
            parametors.Add(p.Key, p.Value);            
        }
        
    }
}
