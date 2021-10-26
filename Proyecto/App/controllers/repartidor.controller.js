const Repartidor = require('../models/repartidor');

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

repartidor_ctrl.createRepartidor = async(req, res) => {};

module.exports = repartidor_ctrl;
