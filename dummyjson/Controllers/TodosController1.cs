using dummyjson.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dummyjson.Controllers
{
	public class TodoController : Controller
	{
		private readonly HttpClient _httpClient;

		public TodoController()
		{
			_httpClient = new HttpClient();
		}

		public async Task<IActionResult> Index()
		{
			var response = await _httpClient.GetStringAsync("https://dummyjson.com/todos");
			var jsonObject = JObject.Parse(response);
			var todosJson = jsonObject["todos"].ToString();
			var todos = JsonConvert.DeserializeObject<List<Todo>>(todosJson);
			return View(todos);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Todo todo)
		{			
				var json = JsonConvert.SerializeObject(todo);
				var content = new StringContent(json, Encoding.UTF8, "application/json");
				var response = await _httpClient.PostAsync("https://dummyjson.com/todos/add", content);
				if (response.IsSuccessStatusCode)
				{
					return RedirectToAction(nameof(Index));
				}			    
			    return View(todo);
		}


	}
}
