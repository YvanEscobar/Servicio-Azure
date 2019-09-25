using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiRest.Models;

namespace WebApiRest.Controllers
{
    public class PersonController : ApiController
    {
        /// <summary>
        /// Obtiene todas las personas
        /// </summary>
        /// <returns></returns>
        // GET: api/Person
        public ArrayList Get()
        {
            PersonPersistence pp = new PersonPersistence();
            return pp.Persons;
        }

        /// <summary>
        /// Obtiene una sola persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Person/5
        public Person Get(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            Person person = pp.getPerson(id);
            if (person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }


        /// <summary>
        /// Obtiene una sola persona
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        // GET: api/Person?lastName=Smith&firstName=Bob
        public Person Get(String firstName, String lastName)
        {
            PersonPersistence pp = new PersonPersistence();
            Person person = pp.getPerson(firstName, lastName);
            
            if (person == null)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
            return person;
        }


        /// <summary>
        /// Registra una persona
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // POST: api/Person
        public HttpResponseMessage Post([FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            long id;
            id = pp.savePerson(value);

            value.ID = id;
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            response.Headers.Location = new Uri(Request.RequestUri, string.Format("person/{0}", id));
            return response;
        }

        /// <summary>
        /// Actualiza una persona
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        // PUT: api/Person/5
        public HttpResponseMessage Put(long id, [FromBody]Person value)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;
            recordExisted = pp.updatePerson(id, value);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;

        }

        /// <summary>
        /// Borra a una persona
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Person/5
        public HttpResponseMessage Delete(long id)
        {
            PersonPersistence pp = new PersonPersistence();
            bool recordExisted = false;

            recordExisted = pp.deletePerson(id);

            HttpResponseMessage response;

            if (recordExisted)
            {
                response = Request.CreateResponse(HttpStatusCode.NoContent);
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.NotFound);
            }
            return response;
        }
    }
}
