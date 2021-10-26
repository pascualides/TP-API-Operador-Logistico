var Repartidor = require('../models/repartidor');
const config = require('./config'); 

const repartidor_ctrl = {};

repartidor_ctrl.getRepartidores = async(req, res) => {
   
    res.status(404).json([{nombre:"Nombre", apellido:"apellido"}, {nombre:"otro nombre", apellido:"otro apellido"}]);
};

repartidor_ctrl.getRepartidor = async(req, res) => {
    //const grabacion = await Grabacion.findById(req.params.id);
};

repartidor_ctrl.updateRepartidor = async(req, res) => {
    //const grabacion = await Grabacion.findById(req.params.id);
};

repartidor_ctrl.deleteRepartidor = async(req, res) => {
    //const grabacion = await Grabacion.findById(req.params.id);
};

repartidor_ctrl.createRepartidor = async(req, res) => {

    var repartidor= new Repartidor();
    repartidor.nombre = req.body.nombre;
    repartidor.apellido= req.body.apellido;
    repartidor.email = req.body.email;
    repartidor.documento = req.body.documento;
    
    sql.connect(config, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query('insert into repartidores(documento, nombre, apellido, email) values (1, "rodrigo", "Pascua", "unmail@gmail.com")', (err, recordset) => {
            if (err) {
                
            }
            console.log(recordset);
        });
    });
};

module.exports = repartidor_ctrl;
