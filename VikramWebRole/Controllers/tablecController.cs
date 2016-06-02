using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VikramWebRole.Models;

namespace VikramWebRole.Controllers
{
    public class tablecController : Controller
    {
        ConnectionString ConnectionObj = new ConnectionString();
        // GET: tablec
        public ActionResult Index()
        {
            return View();
        }

        #region Table Storage Implementation
        public ActionResult CreateTable(string pkey = "", string rkey = "")
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<TableModel>(pkey, rkey);

            TableResult retrievedResult = ConnectionObj.Table.Execute(retrieveOperation);
            TableModel objProp = new TableModel();
            if (retrievedResult.Result != null)
            {

                objProp.ContactNo = pkey;
                objProp.Email = rkey;
                objProp.ContactNo = ((TableModel)retrievedResult.Result).ContactNo;
                objProp.FName = ((TableModel)retrievedResult.Result).FName;
                objProp.LName = ((TableModel)retrievedResult.Result).LName;
                objProp.Address = ((TableModel)retrievedResult.Result).Address;
            }
            return View(objProp);
        }
        [HttpPost]
        public ActionResult InsertTable(TableModel TabControl)
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<TableModel>(TabControl.ContactNo, TabControl.Email);
            TableResult retrievedResult = ConnectionObj.Table.Execute(retrieveOperation);
            TableModel updateEntity = (TableModel)retrievedResult.Result;
            if (updateEntity == null)
            {
                ConnectionObj.Table.CreateIfNotExists();

                TableModel objTab = new TableModel(TabControl.ContactNo, TabControl.Email);
                objTab.FName = TabControl.FName;
                objTab.LName = TabControl.LName;
                objTab.Address = TabControl.Address;
                objTab.ContactNo = TabControl.ContactNo;
                TableOperation insertOperation = TableOperation.Insert(objTab);
                ConnectionObj.Table.Execute(insertOperation);
            }
            else
            {
                updateEntity.FName = TabControl.FName;
                updateEntity.LName = TabControl.LName;
                updateEntity.Address = TabControl.Address;
                updateEntity.ContactNo = TabControl.ContactNo;
                TableOperation updateOperation = TableOperation.Replace(updateEntity);
                ConnectionObj.Table.Execute(updateOperation);
            }

            return RedirectToAction("TableStorage");
        }
        public ActionResult TableStorage()
        {
            TableQuery<TableModel> query = new TableQuery<TableModel>();
            List<TableModel> tableList = new List<TableModel>();
            if (ConnectionObj.Table.Exists())
            {
                foreach (TableModel entity in ConnectionObj.Table.ExecuteQuery(query))
                {
                    TableModel obj = new TableModel();
                    obj.ContactNo = entity.PartitionKey;
                    obj.Email = entity.RowKey;
                    obj.FName = entity.FName;
                    obj.LName = entity.LName;
                    obj.Address = entity.Address;
                    obj.ContactNo = entity.ContactNo;
                    tableList.Add(obj);
                }
            }
            return View(tableList);
        }
        public ActionResult DeleteTable(string pkey = "", string rkey = "")
        {
            TableOperation retrieveOperation = TableOperation.Retrieve<TableModel>(pkey, rkey);
            TableResult retrievedResult = ConnectionObj.Table.Execute(retrieveOperation);
            TableModel deleteEntity = (TableModel)retrievedResult.Result;
            if (deleteEntity != null)
            {
                TableOperation deleteOperation = TableOperation.Delete(deleteEntity);
                ConnectionObj.Table.Execute(deleteOperation);
            }
            return RedirectToAction("TableStorage");
        }
        #endregion
    }
}