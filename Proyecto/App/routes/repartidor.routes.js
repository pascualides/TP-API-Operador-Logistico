const express = require('express');

const router = express.Router();

const repartidor_ctrl = require('../controllers/repartidor.controller')

router.get('/', repartidor_ctrl.getRepartidores);

router.get('/:id', repartidor_ctrl.getRepartidor);

router.delete('/:id', repartidor_ctrl.deleteRepartidor);

router.post('/', repartidor_ctrl.createRepartidor);

router.post('/:id', repartidor_ctrl.updateRepartidor);

module.exports = router;
