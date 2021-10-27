const direccion = require("./direccion");
const persona = require("./persona");
const producto = require("./producto");

const envio = {
    id = "",
    origen:direccion = "",
    destino:direccion = "",
    contacto:persona = "",
    producto:producto = "",
    estado = "",
    fecha_recepcion = "",
    fecha_entrega = ""

};
module.exports = envio;