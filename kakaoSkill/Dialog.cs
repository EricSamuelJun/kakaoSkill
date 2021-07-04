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
            try {
                Dictionary<string, object> text = new Dictionary<string, object>();
                Dictionary<string, object> simpleText = new Dictionary<string, object>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();
                text.Add("text", this.text);
                simpleText.Add("simpleText", text);
                outputs.Add(simpleText);
                foreach (KeyValuePair<string, object> m in parametors) {
                    data.Add(m.Key, m.Value);
                }
                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
                response.Add("data", data);
            }
            catch(Exception ex) {
                Console.WriteLine("===============================");
                Console.WriteLine(ex.Message+ "\n"+ex.StackTrace);
            }
            return response;
        }
        public override void AddParams(KeyValuePair<string, object> m) {
            KeyValuePair<string, object> p = m;
            parametors.Add(p.Key, p.Value);            
        }

    }
    public class SimpleImageResponse : Response {
        public string url;
        public SimpleImageResponse(string mtext) {
            url = mtext;
            parametors = new Dictionary<string, object>();
        }
        public override Dictionary<string, object> get() {

            Dictionary<string, object> response = new Dictionary<string, object>();
            try {
                Dictionary<string, object> image = new Dictionary<string, object>();
                Dictionary<string, object> simpleImage = new Dictionary<string, object>();
                Dictionary<string, object> data = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();
                image.Add("imageUrl", url);
                image.Add("altText", "대체 텍스트. 노출시 봇 개발자에 컨택 바랍니다.");
                simpleImage.Add("simpleImage", image);
                outputs.Add(simpleImage);
                foreach (KeyValuePair<string, object> m in parametors) {
                    data.Add(m.Key, m.Value);
                }
                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
                response.Add("data", data);
            }
            catch (Exception ex) {
                Console.WriteLine("===============================");
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            return response;
        }
        public override void AddParams(KeyValuePair<string, object> m) {
            KeyValuePair<string, object> p = m;
            parametors.Add(p.Key, p.Value);
        }

    }
    public class CardResponse : Response {


        string title;
        string description;
        Thumbnail thumbnail;
        //Profile profile
        //Social social
        List<Button> buttons;
        //ButtonList buttons;
        public CardResponse(Thumbnail thumbnail, string title = "", string description = "", List<Button> buttons = null) {
            this.thumbnail = thumbnail;
            this.title = title;
            this.description = description;
            if (buttons != null)
                this.buttons = buttons;
        }
        private Dictionary<string, object> getData() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("thumbnail", thumbnail.get());
            if (!string.IsNullOrEmpty(title)) {
                dic.Add("title", title);
            }
            if (!string.IsNullOrEmpty(description))
                dic.Add("description", description);
            if (buttons != null) {
                Console.WriteLine("Button null check");
                if (buttons.Count > 0) {
                    Console.WriteLine("Button count check");
                    List<Dictionary<string, object>> btn = new List<Dictionary<string, object>>();
                    foreach(Button b in buttons) {
                        btn.Add(b.get());
                    }
                    dic.Add("buttons", btn);
                }
            }
            return dic;
        }
        public override Dictionary<string, object> get() {

            Dictionary<string, object> response = new Dictionary<string, object>();
            try {

                Dictionary<string, object> data = new Dictionary<string, object>();
                List<Dictionary<string, object>> outputs = new List<Dictionary<string, object>>();
                Dictionary<string, object> template = new Dictionary<string, object>();
                Dictionary<string, object> basicCard = new Dictionary<string, object>();
                basicCard.Add("basicCard", this.getData());
                outputs.Add(basicCard);
                if (parametors != null) {
                    foreach (KeyValuePair<string, object> m in parametors) {
                        data.Add(m.Key, m.Value);
                    }
                }
                template.Add("outputs", outputs);
                response.Add("version", "2.0");
                response.Add("template", template);
                response.Add("data", data);
            }
            catch (Exception ex) {
                Console.WriteLine("===============================");
                Console.WriteLine(ex.Message + "\n" + ex.StackTrace);
            }
            return response;
        }
        public override void AddParams(KeyValuePair<string, object> m) {
            KeyValuePair<string, object> p = m;
            parametors.Add(p.Key, p.Value);
        }
    }
    public class Thumbnail {
        string imageUrl;
        Link link;
        bool fixedRatio = false;
        int width = -1;
        int height = -1;
        public Thumbnail(string url = "", Link link = null, bool fix = false, int width = -1, int height = -1) {
            imageUrl = url;
            this.link = link;
            this.fixedRatio = fix;
            this.width = width;
            this.height = height;
        }
        public Dictionary<string,object> get() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(imageUrl))
                dic.Add("imageUrl", imageUrl);
            if (link != null)
                dic.Add("link", link.get());
            if(fixedRatio != false && width != -1 && height != -1) {
                dic.Add("fixedRatio", "true");
                dic.Add("width", width);
                dic.Add("height", height);
            }
            return dic;
        }
    }

    public class Link {
        string pc = "";
        string mobile = "";
        string web = "";
        public Link(string pc ="",string mobile = "", string web = "") {
            this.pc = pc;
            this.mobile = mobile;
            this.web = web;
        }
        public Dictionary<string,object> get() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(pc)) {
                dic.Add("pc", pc);
            }
            if (!string.IsNullOrWhiteSpace(mobile)) {
                dic.Add("mobile", mobile);
            }
            if (!string.IsNullOrWhiteSpace(web)) {
                dic.Add("web", web);
            }
            return dic;
        }
    }
    public class ButtonList {
        List<Button> buttons;
        public int Count { get { return buttons.Count; }private set { } }
        public ButtonList() {
            buttons = new List<Button>();
        }
        public void AddButton(Button button) {
            this.buttons.Add(button);
        }
        public Dictionary<string,object> get() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("buttons", buttons);
            return dic;
        }

    }
    public class Button {
        public enum BUTTONACTION {
            webLink = 0,
            message = 1,
            phone = 2,
            block = 3,
            share = 4,
            operatorr =5
        };
        string label;
        BUTTONACTION action;
        string webLinkUrl;
        string messageText;
        string phoneNumber;
        string blockId;
        Dictionary<string, object> extra;
        public Button(string label,BUTTONACTION action = BUTTONACTION.message, string webLinkUrl = "",string messageText = "", string phoneNumber  ="", string blockId = "", Dictionary<string,object> extra = null) {
            this.label = label;
            if(action == BUTTONACTION.webLink && string.IsNullOrWhiteSpace(webLinkUrl)) {
                throw new ArgumentNullException("webLink의 URL이 유효하지 않습니다.");
            }
            if(action == BUTTONACTION.message && string.IsNullOrWhiteSpace(messageText)) {
                throw new ArgumentNullException("messageText Text가 유효하지 않습니다.");
            }
            if (action == BUTTONACTION.phone && string.IsNullOrWhiteSpace(phoneNumber)) {
                throw new ArgumentNullException("phoneNumber Text가 유효하지 않습니다.");
            }
            if (action == BUTTONACTION.block && string.IsNullOrWhiteSpace(messageText) && string.IsNullOrWhiteSpace(blockId)) {
                throw new ArgumentNullException("messageText Text 혹은 Block Id 가 유효하지 않습니다.");
            }
            this.label = label;
            this.action = action;
            this.webLinkUrl = webLinkUrl;
            this.messageText = messageText;
            this.phoneNumber = phoneNumber;
            this.blockId = blockId;
            this.extra = extra;
        }
        public Dictionary<string,object> get() {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("label", label);
            switch (action) {
                case BUTTONACTION.webLink:
                    dic.Add("action", "webLink");
                    dic.Add("webLinkUrl", webLinkUrl);
                    break;
                case BUTTONACTION.message:
                    dic.Add("action", "message");
                    dic.Add("messageText", messageText);
                    break;
                case BUTTONACTION.block:
                    dic.Add("action", "block");
                    dic.Add("messageText", messageText);
                    dic.Add("blockId", blockId);
                    break;
                case BUTTONACTION.share:
                    dic.Add("action", "share");
                    break;
                case BUTTONACTION.phone:
                    dic.Add("action", "phone");
                    dic.Add("phoneNumber", phoneNumber);
                    break;
                case BUTTONACTION.operatorr:
                    dic.Add("action", "operator");
                    break;
            }
            if(extra != null) {
                dic.Add("extra", extra);
            }
            return dic;
        }
    }

}
