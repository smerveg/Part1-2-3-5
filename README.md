Teknik Özellikler: Uygulama ASP.NET Core5 ve MSSQL kullanılarak geliştirilmiştir.

Part1:
İşlemler HelloWorldController sınıfında gerçekleştirilmiştir.


Part2:
İşlemler ProductController sınıfında gerçekleştirilmiştir.

Task2.2:
Asenkron programlama:API bağlantısı gibi uzun süreli işlemler gerçekleştirilirken programın bloklanmadan çalışmasına devam etmesini, diğer görevlerin çalışabilmesini sağlar.


Part3:
İşlemler ProductController sınıfında gerçekleştirilmiştir. 
StartUp.cs dosyasının daha anlaşılabilir kalması için servis kayıtları Extensions dosyasın altında yer alan HelloWorldWebAPIExtension.cs dosyasına eklenmiştir.

Task3.2:
JWT tabalı authentication işlemleri için veritabanında User tablosu oluşturulmuştur.Token işlemleri TokenController sınıfında gerçekleştirilmektedir.

Task3.3:
Global Error Handling işlemleri Middlewares dosyası altında yer alan ExceptionMiddleware sınıfı ile gerçekleştirilmektedir. Loglama işlemleri de yine bu sınıfta yapılmaktadır.

Task3.4:
Response caching işlemi uygulanacak tüm actionlar için aynı ayarları içermesi nedeniyle HelloWorldWebAPIExtension.cs sınıfı içerisinde bir Cache Profile oluşturulmuştur.


Part5:

Taask5.1:
Refactoring işlemi için Services dosyası altında IProductService arayüzü ile ProductService sınıfı oluşturulmuştur.

Task 5.2:
Unit test işlemi için ProductApplication.Test isimli bir xunit projesi oluşturulmuştur. Test işlemleri için MockData dosyası altında MockDataSeed.cs sınıfı oluşturulmuştur.
