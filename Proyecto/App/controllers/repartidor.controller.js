var Repartidor = require('../models/repartidor');
const configdb = require('./config');
const sql = require('mssql');

const repartidor_ctrl = {};

repartidor_ctrl.getRepartidores = async(req, res) => {
   
    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("select * from repartidores", (err, recordset) => {
            if (err) {
                
            }

            if(recordset.rowsAffected[0]){
                res.status(200).json(recordset.recordset);
            }
                
        });

    });
};

repartidor_ctrl.getRepartidor = async(req, res) => {
    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("select * from repartidores where documento = "+req.params.id, (err, recordset) => {
            if (err) {
                
            }

            if(recordset.rowsAffected[0]){
                res.status(200).json(recordset.recordset);
            }
                
        });

    });
};

repartidor_ctrl.updateRepartidor = async(req, res) => {
    var query = "UPDATE repartidores SET documento = "+req.body.documento;
    query += ", nombre = '"+req.body.nombre+"', apellido = '"+req.body.apellido+"', email = '"+req.body.email+"' "
    query += "Where documento = "+req.params.id


    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query(query, (err, recordset) => {
            if (err) {
                
            }

            if(recordset.rowsAffected[0]){
                res.status(200).json({
                    documento:req.body.documento,
                    nombre:req.body.nombre,
                    apellido:req.body.apellido,
                    email:req.body.email
                });
            }
                
        });

    });

};

repartidor_ctrl.deleteRepartidor = async(req, res) => {

    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("delete from repartidores where documento = "+req.params.id, (err, recordset) => {
            if (err) {
                
            }

            if(recordset.rowsAffected[0]){
                res.status(200).json(recordset);
            }
                
        });

    });
};

repartidor_ctrl.createRepartidor = async(req, res) => {

    let repartidor = {
        nombre: req.body.nombre,
        apellido: req.body.apellido,
        email: req.body.email,
        documento: req.body.documento
    };
    
    sql.connect(configdb, (err) => {
        if (err) {
            console.log(err);
        }
        var request = new sql.Request();
    
        request.query("insert into repartidores(documento, nombre, apellido, email) values ("+repartidor.documento+", '"+repartidor.nombre+"', '"+repartidor.apellido+"', '"+repartidor.email+"')", (err, recordset) => {
            if (err) {
                
            }
            if(recordset.rowsAffected[0]){
                res.status(200).json(repartidor);
            }
        });

    });
};

module.exports = repartidor_ctrl;
