﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using VotingIrregularities.Api.Models;

namespace VotingIrregularities.Api.Controllers
{
    /// <summary>
    /// Ruta Formular ofera suport pentru toate operatiile legate de formularele completate de observatori
    /// </summary>
    [Route("api/v1/formular")]
    public class Formular : Controller
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;

        public Formular(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Returneaza versiunea tuturor formularelor sub forma unui array. 
        /// Daca versiunea returnata difera de cea din aplicatie, atunci trebuie incarcat formularul din nou 
        /// </summary>
        /// <returns></returns>
        [HttpGet("versiune")]
        public async Task<ModelVersiune> Versiune()
        {
            return new ModelVersiune { Versiune = await _mediator.SendAsync(new ModelFormular.VersiuneQuery())};
        }


        /// <summary>
        /// Se interogheaza ultima versiunea a formularului pentru observatori si se primeste definitia lui. 
        /// In definitia unui formular nu intra intrebarile standard (ora sosirii, etc). 
        /// Acestea se considera implicite pe fiecare formular.
        /// </summary>
        /// <param name="idformular">Id-ul formularului pentru care trebuie preluata definitia</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<ModelSectiune>> Citeste(string idformular)
        {
            return await _mediator.SendAsync(new ModelFormular.IntrebariQuery {CodFormular = idformular});
        }

    }
}