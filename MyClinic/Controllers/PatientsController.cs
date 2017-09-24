using System.Net;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;
using MyClinic.Models;
using MongoDB.Driver;
using MyClinic.ViewModels;
using AutoMapper;
using MyClinic.Helpers;
using System.Web.Http.Routing;
using System.Collections.Generic;
using System;

namespace MyClinic.Controllers
{
    //[Route("api/patients")]
    public class PatientsController : ApiController
    {
        private readonly DataContext _context;
        private VMFactory _factory;

        public PatientsController()
        {
            _context = new DataContext();
        }
        

        // GET api/patient/1
        [SwaggerOperation("GetByName")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("api/patients/GetByName={name}")]
        public IHttpActionResult GetByName(string name)
        {
            
            if(!string.IsNullOrWhiteSpace(name))
            {
                var models = _context.Patients.Find<PatientModel>(x => x.Name.StartsWith(name)).ToList();
                if (models != null)
                {
                    _factory = new VMFactory(new UrlHelper(Request));
                    var viewModels = new List<PatientViewModel>();
                    models.ForEach(x =>
                    {
                        viewModels.Add(_factory.Create(x));
                    });
                    return Ok(viewModels);
                }
                else
                    return NotFound();

            }
            return NotFound();
        }

        // GET api/patient/"Bob"
        [SwaggerOperation("GetById")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get(string id = "")
        {
            if (!string.IsNullOrWhiteSpace(id))
            {
                var model = _context.Patients.Find<PatientModel>(x => x.Id.Equals(id)).FirstOrDefault();
                if (model != null)
                {
                    _factory = new VMFactory(new UrlHelper(Request));
                    var viewModel = _factory.Create(model);
                    return Ok(viewModel);
                }
                return NotFound();
            }
            else
            {
                var patients = _context.Patients.Find(y => y.Name.Length > 0).ToList<PatientModel>();
                var _factory = new VMFactory(new UrlHelper(Request));
                var viewModels = new List<PatientViewModel>();
                patients.ForEach(x =>
                {
                    viewModels.Add(_factory.Create(x));
                });
                return Ok(viewModels);
            }
            
        }

        // GET api/values
        /*[SwaggerOperation("GetAll")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        public IHttpActionResult Get()
        {
            var patients = _context.Patients.Find(y => y.Name.Length > 0).ToList<PatientModel>();
            var _factory = new VMFactory(new UrlHelper(Request));
            var viewModels = new List<PatientViewModel>();
            patients.ForEach(x =>
            {
                viewModels.Add(_factory.Create(x));
            });
            return Ok(viewModels);
        }*/


        // POST api/values
        [SwaggerOperation("Create")]
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        public IHttpActionResult Post([FromBody]PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                _context.Patients.InsertOne(patient);
                var factory = new VMFactory(new UrlHelper(Request));
                var viewModel = factory.Create(patient);
                return Created(viewModel.Link, viewModel);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        // PUT api/values/5
        [SwaggerOperation("Update")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [Route("{id}")]
        public IHttpActionResult Put(int id, [FromBody]PatientModel patient)
        {
            if (ModelState.IsValid)
            {
                var afterUpdate = _context.Patients.ReplaceOne<PatientModel>(x => x.Id.Equals(id), patient, new UpdateOptions() { IsUpsert = false });
                if (afterUpdate != null && afterUpdate.ModifiedCount == 1)
                    return Ok(afterUpdate);
                else
                    return NotFound();
            }
            else
                return BadRequest(ModelState);
        }

        // DELETE api/values/5
        [SwaggerOperation("Delete")]
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [Route("{id}")]
        public IHttpActionResult Delete(string id)
        {
            var result = _context.Patients.FindOneAndDelete((x => x.Id.Equals(id)));
            //var result = _context.Patients.DeleteOne(x => x.Id == id);
            if (result != null && result.Id.Equals(id))
                return Ok();
            else
                return NotFound();
            
        }

        
    }
}
