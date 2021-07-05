using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace kakaoSkill.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class sayHello : ControllerBase {
        // GET: api/<sayHello>
        [HttpGet]
        public IEnumerable<string> Get() {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nGetMethod Call");
            Console.WriteLine("======================================");
            Console.WriteLine("");
            return new string[] { "value1000", "value2" };
        }

        // GET api/<sayHello>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nGetMethod Call");
            Console.WriteLine("======================================");
            Console.WriteLine("");
            return "value";
        }

        // POST api/<sayHello>
        [HttpPost]
        public dynamic Post([FromBody]JObject vals) {
            Dialog d = new Dialog(vals);
            Dictionary<string, object> response = new Dictionary<string, object>();
            
            try {
                if (d.Utterance.Equals("캬루")) {
                    SimpleImageResponse sir = new SimpleImageResponse("https://img.gigglehd.com/gg/files/attach/images/158/992/761/009/f17907c12b2c4df369dd982a3145cc70.png");
                    response = sir.get();
                } else if (d.Utterance.IndexOf("샌즈") != -1) {
                    List<Button> btns = new List<Button>();
                    btns.Add(new Button(label: "메시지 버튼", messageText: "메시지 응답"));
                    btns.Add(new Button(label: "전화번호 연결해", action: Button.BUTTONACTION.phone, phoneNumber: "01044266736"));
                    btns.Add(new Button(label: "share 버튼", action:Button.BUTTONACTION.share));

                    CardResponse cr = new CardResponse(
                        thumbnail: new Thumbnail(
                            url: "https://file.namu.moe/file/85309fcaaf7e1bb0512e03c1b7c47fe9b59db0daddf33cc9baff362522b91d74"
                            , link: new Link(web: "https://www.youtube.com/watch?v=RFFdvIqzrls&ab_channel=31HorasMusic"))
                        , title: "WA! 샌즈!"
                        , description: "당신은 죄악이 등골을 타고 오르는 것을 느꼈다.",
                        buttons: btns);
                    response = cr.get();
                } else if (d.Utterance.IndexOf("펄럭") != -1) {
                    List<Button> btns = new List<Button>();
                    btns.Add(new Button(label: "블럭 테스트",action: Button.BUTTONACTION.block, messageText: "test 블록 호출",blockId: "60dacdf675f74b7865cc78f7"));
                    btns.Add(new Button(label: "상담사 연결",action: Button.BUTTONACTION.operatorr));
                    btns.Add(new Button(label: "url 연결", action: Button.BUTTONACTION.webLink, webLinkUrl: "https://www.youtube.com/watch?v=dQw4w9WgXcQ&ab_channel=RickAstley"));

                    CardResponse cr = new CardResponse(
                        thumbnail: new Thumbnail(
                            url: "https://w.namu.la/s/43a07e65f573eb41fffe67ac0d1008fa73b5c7a04a004ff9004ddf0680524c5c5bd8a30c724fd7966bd7d3a2f60d0bd17c3cc159dd41f704f9b6dc188a21346d43ba30ae041949e44e3a5fc57c739e3c363bded57e1423e1536dfdba3fc4364b"
                            , link: new Link(web: "https://www.youtube.com/watch?v=n6WaTObHRJM&ab_channel=%EC%97%90%EB%A0%88%EB%A9%9Clmlxiabeize"))
                        , title: "펄~럭"
                        , description: "동해물과 백두산이 마르고 닳도록~",
                        buttons: btns);
                    response = cr.get();
                } else {
                    
                    SimpleTextResponse str = new SimpleTextResponse("지난번 말: "+ DATA.temp + "\n" + "이번 말: " + d.Utterance);
                    str.AddParams(new KeyValuePair<string, object>("level", 5));
                    DATA.temp = d.Utterance;
                    response = str.get();
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message+"\n"+e.StackTrace); 
            }
            Console.WriteLine("============================================" +
                "\nresponse: \n"+
                JObject.FromObject(response).ToString()+
                "\n==============================================");
            return response;

        }

        // PUT api/<sayHello>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
            Console.WriteLine("");
            Console.WriteLine("======================================");
            Console.WriteLine("\nPut Mehtod Call with id: {0}, and value: {1}",id,value);
            Console.WriteLine("======================================");
            Console.WriteLine("");
        }

        // DELETE api/<sayHello>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
