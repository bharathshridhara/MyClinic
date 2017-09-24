using MongoDB.Bson;
using MongoDB.Driver;
using MyClinic.Helpers;
using MyClinic.Models;
using MyClinic.ViewModels;
using Swashbuckle.Swagger.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace MyClinic.Controllers
{
    public class ObservationsController : ApiController
    {
        private readonly DataContext _context;
        private VMFactory _factory;

        public ObservationsController()
        {
            _context = new DataContext();
        }

        [SwaggerOperation("GetByAllObservationsForPatient")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(string patientid)
        {
            var obs = _context.Patients.Find(x => x.Id.Equals(patientid))
                        .Project(y => y.Observations)
                        .SortByDescending(z => z.Observations).ToList();
            _factory = new VMFactory(new UrlHelper(Request));
            var viewModel = _factory.Create(obs, patientid);
            return Ok(viewModel);
        }

        public string Get(string patientid, string id)
        {
            return "value";
        }

        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post(string patientid, [FromBody]ObservationModel obs)
        {
            if (ModelState.IsValid)
            {
                var filter = Builders<PatientModel>.Filter.Eq("Id", patientid);
                var update = Builders<PatientModel>.Update.Push(x => x.Observations, obs);

                var modified = _context.Patients.UpdateOne(filter, update).ModifiedCount ;
                var test = _context.Patients.Find(filter).FirstOrDefault();
                _factory = new VMFactory(new UrlHelper(Request));
                var viewModel = _factory.Create(test);
                return Created(viewModel.Link, viewModel);
            }
            else
                return BadRequest(ModelState);
        }

        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public void Put(string patientid, string id, [FromBody]ObservationModel model)
        {
            var filter = Builders<PatientModel>.Filter.And(
                        Builders<PatientModel>.Filter.Eq("Id", patientid));
                        
            var update = Builders<PatientModel>.Update.PopFirst(x => x.Observations.Any(y => y.ObservationId.Equals(id)))
                                                        .Push(x => x.Observations, model);
            
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Delete(string patientid, string id)
        {
            var filter = Builders<PatientModel>.Filter.Eq("Id", patientid);
            var update = Builders<PatientModel>.Update.PullFilter(x => x.Observations, 
                                f => f.ObservationId.Equals(id));
            var modified = _context.Patients.FindOneAndUpdate(filter, update);
            if(modified != null)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }

        }
    }
}