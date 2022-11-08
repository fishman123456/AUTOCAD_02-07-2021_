
using System;
using System.IO;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
 
namespace HabrPlug_Layers
{
    // Создан 24-12-2021 (спизжен)
    //(vl-cmdf "_netload" "c:/myapps/mycommands.dll") - лиспом dll загружаем
    // "C:\Users\Fishman.DB.CORP\YandexDisk\Работа АСКО\AUTOCAD_Плагины\02-07-2021_\Layer_Add\bin\Debug\Layer_Add.dll"
    // (vl-cmdf "_netload" "C:\Users\Fishman.DB.CORP\YandexDisk\Работа АСКО\AUTOCAD_Плагины\02-07-2021_\Layer_Add\bin\Debug\Layer_Add.dll")
    //https://habr.com/ru/post/249015/
    //https://habr.com/ru/post/235723/
    //
    public class Commands : IExtensionApplication
    {
        // эта функция будет вызываться при выполнении в AutoCAD команды "TestCommand"
        [CommandMethod("Layer_Add")]
        public void MyCommand()
        {
// получаем текущий документ и его БД
            Document acDoc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database acCurDb = acDoc.Database;
            /*Устанавливаем русский шрифт*/
           
            // блокируем документ
            using (DocumentLock docloc = acDoc.LockDocument())
            {
                // начинаем транзакцию
                using (Transaction tr = acCurDb.TransactionManager.StartTransaction())
                {
                  
                    // открываем таблицу слоев документа
                    LayerTable acLyrTbl = tr.GetObject(acCurDb.LayerTableId, OpenMode.ForWrite) as LayerTable;

                    // создаем новый слой и задаем ему имя
                    LayerTableRecord acLyrTblRec = new LayerTableRecord();
       
                    acLyrTblRec.Name = "301.R-K1";
                    // заносим созданный слой в таблицу слоев
                    acLyrTbl.Add(acLyrTblRec);
                    // добавляем созданный слой в документ
                    tr.AddNewlyCreatedDBObject(acLyrTblRec, true);

                    // фиксируем транзакцию
                    tr.Commit();
                }
            }
        }

        // Функции Initialize() и Terminate() необходимы, чтобы реализовать интерфейс IExtensionApplication
        public void Initialize()
        {

        }

        public void Terminate()
        {

        }
    }
}