# .NetCore2.0-LoginState
Bu proje .Net Core 2.0 ile geliştirilmiştir. 
Projeyi çalıştırmak için ;
1. MSSQL üzerinden "New Query" ile alttaki komut Runlanır.


  create table TblUser
  
    (  
  
      Id int,

      Name nvarchar(35),

      Username nvarchar(35),

      Passwordd nvarchar(35),

      Mail nvarchar(35),

      Addresskey int
    
    );

2. "Models" klasörü içindeki UserDataAccessLayer.cs sınıfının içindeki Connectionstring'e oluşturulan tablonun connection string i girilir.

-------------------------------------------------------------------
Proje  /Login üzerinden giriş kontrol edilir ve doğru olduğunda session kullanıcının gerekli verilerini kaydeder . Bu işlem LoginController üzerinde olur.
İlgili kod :
---------------
 [HttpPost]
        public IActionResult LoginState()
        {
             
            try
            {
                bool statususer = false;
                List<User> userlist = new List<User>();
                userlist = objuser.GetAllUser().ToList();
                String Tempusername = HttpContext.Request.Form["username"].ToString();
                string Temppassword =HttpContext.Request.Form["password"].ToString();
                foreach (var item in userlist)
                {
                    if (item.Username == Tempusername && item.Passwordd ==Temppassword && SessionLyr.Name==null)
                    {
                        statususer = true;
                        Id = item.Id;
                        Name = item.Name;
                        Username = item.Username;
                        Password = item.Passwordd;
                        Mail = item.Mail;
                        Addresskey = item.Addresskey;
                        break;
                    }
                }
                if (statususer)
                {
                    SessionLayer sesiontemp = new SessionLayer { Id = Id, Addresskey = Addresskey, Name = Name.ToString(), Username = Username.ToString(), Passwordd = Password.ToString(), Mail = Mail.ToString() };
                    HttpContext.Session.Set("session",sesiontemp);

                    ViewBag.Result = "Kullanıcı adı veya sifre Dogru";

                }
                else
                {
                    ViewBag.Result = "Kullanıcı adı veya sifre hatalı";
                }
                
            }
            catch (Exception)
            {
                ViewBag.Result = "Erişim hatası ";
            }
            return View("Index");
        }
        
        
        ---------------------------------------------------------------
        
        
        
  Çalışma mantığı gayet açık. userlist user Tipinde oluşturulmuş bir liste yani içinde DataAccessLayer (DAL) sayesinde elde ettiğimiz veriler var.
Session değerimiz boşsa ve View/Login/Index.html sayfasından  form yardımıyla gelen değerlerin username ve password alanları eşleşirse status true olacak ve bir alt case içinde Session verileri eklenecek.
Ayrıca ilk çalıştırmada form aracılığı ile post edilen veriler .Net core tarafında bir problemle karşılaşıp Post metoduna redirect oluyorlar. Enteresan bir problem ancak 2.derlemede problem ortadan kalkıyor.
        
        
        
        
        ////////////////////////////-
Şimdi can alıcı problemim olan Peki biz diğer controller'de bu adam login olmuşmu nasıl bileceğiz?
**BASİTMİŞ**
Basitmiş ama HomeController  bir constructer yardımıyla Home/About ta denek için bir case yaptım . 

      /////////////////////////////////////////-
      
Buraya kadar herşey çok kolay ve mantıklı peki ya Layout ta nolcak. 
Layout tüm sayfalarda tekrarı önlemek için adamların bir tasarım yapayım heryere koyayım felsefesinden gelen aslında çok başarılı ama başta karmaşık .cshtml dosyası.
O layout üzerinde session bilgilerinin bir kısmı gösterilmeli , Logout buttonu olmalı vs.
Ama ben yapamadım çok uğraştım. TempData[""] yazınca geliyodu ama bi kaç ingiliz amca TempData Tehlikeli Çalarlar demiş anlatmış . 
Bide zaten saçma, Kullanıcı /Home controllerinde /Sales controller ine geçeçek Session verisini taşıyan ayrı bir TempData oluşturma gereği doğuyordu.


Layout.cshtml içine c# kodları halihazırda yazılıyor ancak consturucter oluşmuyor yada ben beceremedim neyse bu sorunuda ;

                  @{


                var sesion = HttpContextAccessor.HttpContext.Session.GetString("session");
                var sesionoj = Newtonsoft.Json.Linq.JObject.Parse(sesion); // parse as array
                var name = sesionoj.Property("Name");
                if (name == null)
                {
                        @name.Value;
                        <li ><a href="~/../Views/Login/Index.cshtml">Giriş YAP</a></li>
                }
                if (name != null)
                {
                   
                    @name.Value;
                    }

                    }
Bu şekilde hallettim . Bu if state ile istediğiniz tüm  form elementleri açabilirsiniz. 
Tabiki bu mantığı anlamak için Jquery bunun için çok iyi bir alternatif olabilir.



--------------------------------
