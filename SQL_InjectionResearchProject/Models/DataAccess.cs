using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SQL_InjectionResearchProject.Models
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DataAccess : Controller 
    {
        private readonly string connectionstring = "Server=studmysql01.fhict.local;Uid=dbi457726;Database=dbi457726;Pwd=Banaan12;";

        [HttpPost]
        public void AddData(string productname, string description, string brand, int price)
        {
            using MySqlConnection con = new(connectionstring);
            con.Open();

            MySqlCommand cmd = new($"INSERT INTO `product` (`ProductName`, `Description`, `Brand`, `Price`) VALUES (?productname, ?description, ?brand, ?price) ", con);
            cmd.Parameters.AddWithValue("?productname", productname);
            cmd.Parameters.AddWithValue("?description", description);
            cmd.Parameters.AddWithValue("?brand", brand);
            cmd.Parameters.AddWithValue("?price", price);

            MySqlDataReader reader = cmd.ExecuteReader();
            con.Close();
        }

        public void UpdateData(string productname, string description, string brand, int price, int id)
        {
            using MySqlConnection con = new(connectionstring);
            con.Open();

            MySqlCommand cmd = new($"UPDATE `product` SET `ProductName`=?productname,`Description`=?description,`Brand`=?brand,`Price`=?price WHERE `Id`=?Id", con);
            cmd.Parameters.AddWithValue("?productname", productname);
            cmd.Parameters.AddWithValue("?description", description);
            cmd.Parameters.AddWithValue("?brand", brand);
            cmd.Parameters.AddWithValue("?price", price);
            cmd.Parameters.AddWithValue("?Id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
        }

        public void DeleteData(int id)
        {
            using MySqlConnection con = new(connectionstring);
            con.Open();

            MySqlCommand cmd = new($"DELETE FROM `product` WHERE `Id` =?id", con);
            cmd.Parameters.AddWithValue("?id", id);

            MySqlDataReader reader = cmd.ExecuteReader();
        }
        [HttpGet]
        public string GetProductData(int id)
        {
            DataModel data = new();

            using MySqlConnection con = new(connectionstring);
            con.Open();

            MySqlCommand cmd = new($"SELECT * FROM `product` WHERE Id=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                data.ProductName = reader["ProductName"].ToString();
                data.Description = reader["Description"].ToString();
                data.Brand = reader["Brand"].ToString();
                data.Price = int.Parse(reader["Price"].ToString());
                data.Id = int.Parse(reader["Id"].ToString());
            }
            reader.Close();
            Console.WriteLine(data);
            var json = JsonConvert.SerializeObject(data);
            return (json);
        }
        [HttpGet]
        public string GetData()
        {
            List<DataModel> data = new();

            using MySqlConnection con = new(connectionstring);
            con.Open();

            MySqlCommand cmd = new($"SELECT * FROM `product`", con);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                DataModel datas = new();
                datas.ProductName = reader["ProductName"].ToString();
                datas.Description = reader["Description"].ToString();
                datas.Brand = reader["Brand"].ToString();
                datas.Price = int.Parse(reader["Price"].ToString());
                datas.Id = int.Parse(reader["Id"].ToString());
                data.Add(datas);
            }
            reader.Close();
            Console.WriteLine(data);
            var json = JsonConvert.SerializeObject(data);
            return (json);
        }

    }
}
