import { combineReducers } from 'redux';

// Import custom components
import articuloReducer from './articulos';
import cartReducer from './cart';
import compareReducer from './compare';
import modalReducer from './modal';
import marcaReducer from './marcas'

const rootReducer = combineReducers( {
    data: articuloReducer,
    cartlist: cartReducer,
    compare: compareReducer,
    modal: modalReducer,
    marcas: marcaReducer,
} );

export default rootReducer;