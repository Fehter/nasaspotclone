using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using SPOT_App.ViewModels;

namespace SPOT_App
{
    public class RestService
    {
        HttpClient client;

        RequestViewModel rvm;

        public RestService()
        {
            client = new HttpClient();

        }

        public async void TestPOST()
        {
            int x = 7;

            rvm = new RequestViewModel
            {
                Name = "123Name" + x,
                OrganizationName = "123OrgName" + x,
                Email = "123Email" + x,
                PrimaryPhoneNumber = "test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber test PrimaryPhoneNumber " + x,
                AlternativePhoneNumber = "test AlternativePhoneNumber " + x,
                ContactTimes = "test ContactTimes " + x,
                PresentationLocation = "test PresentationLocation " + x,
                PresentationRequested = "test PresentationRequested " + x,
                PresentationRotations = "test PresentationRotations " + x,
                HandsOnActivity = "test HandsOnActivity " + x,
                GradeLevels = "test GradeLevels " + x,
                NumberOfStudents = "test NumberOfStudents " + x,
                ProposedDateAndTime = "test ProposedDateAndTime " + x,
                Supplies = "test Supplies " + x,
                TravelFee = "test TravelFee " + x,
                AmbassadorStatus = "test AmbassadorStatus " + x,
                OtherConcerns = "test OtherConcerns " + x,
                PresentationAlternativeMethod = "test PresentationAlternativeMethod " + x
            };

            //var uri = new Uri("http://192.168.1.126:80/test/application_files/get_request_data.php");

            var uri = new Uri("http://192.168.1.126:80/test/application_files/test_set_request_data.php");

            try
            {
                client.MaxResponseContentBufferSize = 2560000;

                //string data = "testKey=testValue";
                //data = rvm.ToString();

                Debug.WriteLine("*****TESTING***CONTENT***START*****");
                //Debug.WriteLine(rvm.GetContents());
                //Debug.WriteLine(rvm.Name);
                //Debug.WriteLine(data);
                //StringContent content = new StringContent(rvm.GetContents());
                //StringContent content = new StringContent(data);
                //string contentContents = await content.ReadAsStringAsync();
                //Debug.WriteLine(rvm.GetStringContent());

                HttpResponseMessage response = await client.PostAsync(uri, rvm.GetFormUrlEncodedContent());

                Debug.WriteLine("*****TESTING***CONTENT***END*****");
                Debug.WriteLine("");
                Debug.WriteLine("*****TESTING***CONNECTION***START*****");

                Debug.WriteLine(response.GetType());
                Debug.WriteLine(response.Content.GetType());
                string responseContent = await response.Content.ReadAsStringAsync();
                Debug.WriteLine(responseContent);

                Debug.WriteLine("*****TESTING***CONNECTION***END*****");
            }

            catch (Exception e)
            {
                Debug.WriteLine(e);
                Debug.Fail("Something went wrong while testing connection to web service!");
            }
        }
    }
}
