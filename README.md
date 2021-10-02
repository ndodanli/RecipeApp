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

**Not:** MongoDB'nin locale alınmasına karşın, uygulamaya admin ve moderator rollerini hesapları ile seedleyen bir data seeder yazılmıştır(tarifler için seed yapılmadı).

**Moderatör özellikleri**

- Yemek tarifi oluşturabilir, görebilir, güncelleyebilir ve silebilir.
- Kategori oluşturabilir, görebilir, güncelleyebilir ve silebilir.


**Redis**

Redis:
>Configuration: 127.0.0.1:6379

**MongoDB**

- MongoDB benim veri tabanıma bağlıdır, appsettings.json dosyasından değiştirilip locale alınabilir.

Örnek recipe verisi:
>{
    "_id": {
        "$oid": "615671cc2b55cb794b00b7ee"
    },
    "Name": "Apple Crisp",
    "ImagePath": "/images/1de565ea-39c0-4ae0-bddc-06129a5fd6ca_image.jfif",
    "Slug": "apple-crisp",
    "View": 11,
    "Servings": 4,
    "Directions": "Preheat oven to 350 degrees F (175 degree C).Place the sliced apples in a 9x13 inch pan. Mix the white sugar, 1 tablespoon flour and ground cinnamon together, and sprinkle over apples. Pour water evenly over all.Combine the oats, 1 cup flour, brown sugar, baking powder, baking soda and melted butter together. Crumble evenly over the apple mixture.Bake at 350 degrees F (175 degrees C) for about 45 minutes.",
    "Ingredients": ["10 cups all-purpose apples, peeled, cored and sliced", "1 cup white sugar", "1 teaspoon ground cinnamon", "½ cup water", "1 cup quick-cooking oats", "1 cup all-purpose flour"],
    "CategoryId": "615671822b55cb794b00b7ed"
}

Örnek category verisi:
>{
    "_id": {
        "$oid": "615671822b55cb794b00b7ed"
    },
    "Name": "Dessert"
}


Örnek role verisi:
>{
    "_id": {
        "$oid": "6157744455232c483eb296bf"
    },
    "Name": "admin",
    "Permissions": ["admin.addrole", "admin.addrecipe", "admin.index", "admin.getroles", "admin.updaterole", "admin.deleterole", "admin.roles", "admin.recipes", "admin.getrecipes", "admin.getusers", "admin.adduser", "admin.updateuser", "admin.deleteuser", "admin.deleterecipe", "admin.updaterecipe", "admin.categories", "admin.addcategory", "admin.updatecategory", "admin.deletecategory", "admin.getcategories"]
}


Örnek user verisi:
>{
    "_id": {
        "$oid": "6157744555232c483eb296c1"
    },
    "Username": "admin",
    "Password": "$2b$10$94Dq41sDy2SL/UIT0aLiZOmmK1RKtCXlVzVnGFaNADBczTGVl29bG",
    "RoleId": "6157744455232c483eb296bf"
}

**Not:** Rollerin yetkileri metod bazlı belirlenebilir(örn. ControllerName1.MethodName1,ControllerName2.MethodName2,ControllerName3.MethodName3).

**Not:** Hataları görmek için pop-upları açmanız gerekiyor(Hatalar pop-up olarak gönderildi, örn. yetkilendirme hataları).
