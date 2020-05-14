using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

public class test : MonoBehaviour
{
    MySqlConnection sqlconn = null;
    private string sqlDBip = "DB아이피";
    private string sqlDBname = "DB네임";
    private string sqlDBid = "관리자계정ID";
    private string sqlDBpw = "관리자계정비번";

    private void sqlConnect()
    {
        //DB정보 입력
        string sqlDatabase = "Server=" + sqlDBip + ";Database=" + sqlDBname + ";UserId=" + sqlDBid + ";Password=" + sqlDBpw + "";

        //접속 확인하기
        try
        {
            sqlconn = new MySqlConnection(sqlDatabase);
            sqlconn.Open();
            Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 되면 OPEN이라고 나타남
        }
        catch (Exception msg)
        {
            Debug.Log(msg); //기타다른오류가 나타나면 오류에 대한 내용이 나타남
        }
    }

    private void sqldisConnect()
    {
        sqlconn.Close();
        Debug.Log("SQL의 접속 상태 : " + sqlconn.State); //접속이 끊기면 Close가 나타남 
    }

    private void Start()
    {
        sqlConnect();
    }
}
