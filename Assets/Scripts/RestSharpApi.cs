using UnityEngine;
using RestSharp;
using System;
using Newtonsoft.Json;
using UnityEngine.UI;
using TMPro;

public class RestSharpApi : MonoBehaviour
{
    private RestClient client = null;
    private string loginurl = "http://www.officewrk.online/api/Login";//url paste here
    private string registerurl = "http://www.officewrk.online/api/Register";//also register url
    [SerializeField] private Button loginButton;
    private string memberId = "Game";
    private string password = "1234";
    //memberId/username: OG312055
    //password: 1234

    public RestSharpApi()
    {
        client = new RestClient();
    }

    public void Login()
    {
        var res = GetLogin(memberId, password);
        Debug.Log(res.Mid);
    }

    public void Register()
    {
        var res = GetRegistration("Tester", "tester@gmail.com", "987456123", "uks", "Game", "1234");
        Debug.Log(res.Mid);
        Debug.Log(res.Memberid);
    }

    public RegisterModel GetLogin(string memberid, string password)
    {
        client.BaseUrl = new Uri(loginurl);
        client.Timeout = -1;
        client.FollowRedirects = false;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");

        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        request.AddParameter("Memberid", memberid);
        request.AddParameter("Password", password);

        IRestResponse response = client.Execute(request);
        // return response;
        return ConvertToArray(response.Content);
    }

    public RegisterModel GetRegistration(string username, string emailid, string mobileno, string country, string referral, string password)
    {
        client.BaseUrl = new Uri(registerurl);
        client.Timeout = -1;
        client.FollowRedirects = false;
        var request = new RestRequest(Method.POST);
        request.AddHeader("Content-Type", "application/json");
        request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

        request.AddParameter("Username", username);
        request.AddParameter("Emailid", emailid);
        request.AddParameter("Mobileno", mobileno);
        request.AddParameter("Country", country);
        request.AddParameter("Password", password);
        request.AddParameter("Referralid", referral);

        IRestResponse response = client.Execute(request);
        // return response;
        return ConvertToArray(response.Content);
    }



    private RegisterModel ConvertToArray(string transaction)
    {

        RegisterModel result = JsonConvert.DeserializeObject<RegisterModel>(transaction);
        return result;
    }

}
public partial class RegisterModel
{
    public string Message { set; get; }
    public Int64 Mid { set; get; }
    public string Authorized { set; get; }
    public string Referralid { set; get; }
    public string Username { set; get; }
    public string Emailid { set; get; }
    public string Mobileno { set; get; }
    public string Country { set; get; }
    public string Memberid { set; get; }
    public string Password { set; get; }
}