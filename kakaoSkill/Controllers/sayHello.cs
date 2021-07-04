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
