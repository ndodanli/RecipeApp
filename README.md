# RecipeApp

**Home Page**

Ana sayfa url(IIS ile çalıştırıldığında, port:44352):
>https://localhost:44352/

Recipe url(IIS ile çalıştırıldığında, port:44352):
>https://localhost:44352/recipe/slugName


**Admin Page**

Admin ana sayfa url(IIS ile çalıştırıldığında, port:44352):
>https://localhost:44352/admin

Admin login sayfası url(IIS ile çalıştırıldığında, port:44352):
>https://localhost:44352/admin/login

Admin hesap bilgileri:
>Kullanıcı adı: admin

>Kullanıcı parolası: admin

Moderatör hesap bilgileri:
>Kullanıcı adı: moderator

>Kullanıcı parolası: moderator

**Not:** Eğer MongoDB locale alınacaksa, uygulamaya admin ve moderator rollerini hesapları ile seedleyen bir data seeder yazılmıştır(tarifler için seed yapılmadı).

**Moderatör özellikleri**

- Yemek tarifi oluşturabilir, görebilir, güncelleyebilir ve silebilir.
- Kategori oluşturabilir, görebilir, güncelleyebilir ve silebilir.


**Redis**

Redis:
>Configuration: 127.0.0.1:6379

**MongoDB**

- MongoDB benim veri tabanıma bağlıdır, appsettings.json dosyasından değiştirilip 

e alınabilir.

**Not:** Rollerin yetkileri metod bazlı belirlenebilir(örn. ControllerName1.MethodName1,ControllerName2,MethodName2,ControllerName3,MethodName3).

**Not:** Hataları görmek için pop-upları açmanız gerekiyor(Hatalar pop-up olarak gönderildi).
