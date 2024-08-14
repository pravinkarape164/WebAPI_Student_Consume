using Microsoft.AspNetCore.Mvc;
using WebAPI_Student_Callingusingcore.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace WebAPI_Student_Callingusingcore.Controllers
{
    public class StudentController : Controller
    {
        private readonly string _url = "https://localhost:7292/api/StudentAPI/";
        private readonly HttpClient _httpClient;

        public StudentController()
        {
            _httpClient = new HttpClient();
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Student> students = new List<Student>();

            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_url);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = response.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<List<Student>>(responseData);
                    if (data != null)
                    {
                        students = data;
                    }
                }
                else
                {
                    response.ToString();
                }
            }
            catch (HttpRequestException ex)
            {

                // Handle request exception
                // e.g., log error, return error view, etc.
            }

            return View(students);
        }
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(Student student)
        {
            string JsonData = JsonConvert.SerializeObject(student);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_url,content).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Student Added Successfully";
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
           Student student = new Student();
            HttpResponseMessage response = _httpClient.GetAsync(_url + id).Result;

            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student)
        {
            string data =JsonConvert.SerializeObject(student);
            StringContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PutAsync(_url +student.Id,stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Student Updated Successfully";
                return RedirectToAction("Index");
            }
            return View() ;

        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = _httpClient.GetAsync(_url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                 string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            Student student = new Student();
            HttpResponseMessage response = _httpClient.GetAsync(_url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<Student>(result);
                if (data != null)
                {
                    student = data;
                }
            }
            return View(student);
        }
        [HttpPost,ActionName("Delete")]
        public IActionResult DeleteStudent(int id)
        {
            HttpResponseMessage response = _httpClient.DeleteAsync(_url + id).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = "Student record deleted";
                return RedirectToAction("Index");
            }
            return View();
        }
        
    }
}
