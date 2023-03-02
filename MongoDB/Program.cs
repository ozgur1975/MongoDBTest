// See https://aka.ms/new-console-template for more information
using MongoDB;




var t1 = new test1();
await t1.AtlasTest();  // atlas üzerinden yapıyor
 await t1.CreateDocument(); // local veritabanından yapıyor

Console.WriteLine("Hello, World!");

//var t2 = new transactionmongo();
//await t2.startTrn();
