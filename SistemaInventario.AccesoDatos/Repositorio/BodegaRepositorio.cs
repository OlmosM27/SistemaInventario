﻿using SistemaInventario.AccesoDatos.Data;
using SistemaInventario.AccesoDatos.Repositorio.IRepositorio;
using SistemaInventario.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaInventario.AccesoDatos.Repositorio
{
    public class BodegaRepositorio : Repositorio<Bodega>, IBodegaRepositorio
    {
        private readonly ApplicationDbContext _db;
        public BodegaRepositorio(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Bodega bodega)
        {
            var bodegaBD = _db.Bodegas.FirstOrDefault(b =>  b.Id == bodega.Id);
            if (bodegaBD != null)
            {
                bodegaBD.Name = bodega.Name;
                bodegaBD.Description = bodega.Description;
                bodegaBD.Status = bodega.Status;
                _db.SaveChanges();
            }
        }
    }
}
