using AutoMapper;
using MyClinic.Models;
using MyClinic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Routing;
using MongoDB.Driver;
using System.Web.Http;

namespace MyClinic.Helpers
{
    public class VMFactory
    {
        private UrlHelper _url;

        public VMFactory(UrlHelper url)
        {
            _url = url;
        }

        [Authorize]
        public PatientViewModel Create(PatientModel model)
        {
            var viewModel = Mapper.Map<PatientViewModel>(model);
            viewModel.Link = _url.Link("Patients", new { id = model.Id });
            viewModel.ObservationsLink = _url.Link("Observations", new { patientid = model.Id }); ;
            viewModel.Observations = Create(model.Observations.ToList(), model.Id);
            return viewModel;
        }

        public List<ObservationViewModel> Create(List<ObservationModel> observations, string patientId)
        {
            var viewModels = Mapper.Map<IEnumerable<ObservationViewModel>>(observations)
                            .ToList();

            viewModels.ForEach(z => z.PatientsLink = _url.Link("Patients", new { id = patientId }));
            return viewModels.ToList();
        }

        internal List<ObservationViewModel> Create(IEnumerable<ObservationModel[]> obs, string patientid)
        {
            var items = new List<ObservationViewModel>();
            foreach(var item in obs)
            {
                items = Mapper.Map<IEnumerable<ObservationViewModel>>(item).ToList();
                items.ForEach(z => {
                    z.PatientsLink = _url.Link("Patients", new { id = patientid });
                    z.ObservationsLink = _url.Link("Observations", new { patientid = patientid, id = z.ObservationId });
                });
                break;
            }
            return items;
        }

        internal object Create(IOrderedFindFluent<PatientModel, ObservationModel[]> obs, string patientid)
        {
            throw new NotImplementedException();
        }
    }
}