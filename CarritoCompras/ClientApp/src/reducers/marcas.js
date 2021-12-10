import { RECEIVE_MARCAS } from "../constants/action-types";
import { findIndex } from "../utils";

import { persistReducer } from "redux-persist";
import storage from 'redux-persist/lib/storage';

const initialState = {
    marcas: []
};

const marcasReducer = ( state = initialState, action ) => {
    
    switch ( action.type ) {
        case RECEIVE_MARCAS:
            return {
                ...state,
                marcas: action.marcas
                
            };

        default:
            return state;
    }

    
};
const persistConfig = {
    keyPrefix: "molla-",
    key: "marcas",
    storage
}

export default persistReducer( persistConfig, marcasReducer );