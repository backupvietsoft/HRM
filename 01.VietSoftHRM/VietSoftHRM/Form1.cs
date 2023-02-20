using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace VietSoftHRM
{
    public partial class Form1 : Form
    {
        private const string ApiKey = "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";
        private const string Endpoint = "https://api.openai.com/v1/completions";
        public Form1()
        {
            InitializeComponent();
        }
        private async Task<string> SendRequest(string input)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
            
            var requestBody = new
            {
                model = "text-davinci-003",
                prompt = input,
                max_tokens = 60,
                temperature = 0.5,
            };
            var response = await httpClient.PostAsJsonAsync(Endpoint, requestBody);
            var responseBody = await response.Content.ReadAsAsync<CompletionResponse>();
            return responseBody.choices[0].text;
        }

        private class CompletionResponse
        {
            public Choice[] choices { get; set; }
        }

        private class Choice
        {
            public string text { get; set; }
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            var input = InputTextBox.Text;
            var output = SendRequest(input);
            OutputListBox.Items.Add(output);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
    }
}
