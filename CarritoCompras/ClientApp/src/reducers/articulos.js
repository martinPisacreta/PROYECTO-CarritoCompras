import { RECEIVE_ARTICULOS, SHOW_QUICKVIEW, CLOSE_QUICKVIEW } from "../constants/action-types";
import { findIndex } from "../utils";

import { persistReducer } from "redux-persist";
import storage from 'redux-persist/lib/storage';

const initialState = {
    articulos: [],
    articuloDetail: [],
    quickView: false
};

const articuloReducer = ( state = initialState, action ) => {
    switch ( action.type ) {
        case RECEIVE_ARTICULOS:
            return {
                ...state,
                articulos: action.articulos
            };

        case SHOW_QUICKVIEW:
            let index = findIndex( state.articulos, articulo => articulo.id === action.articuloId );
            if ( -1 !== index ) {
                const item = state.articulos[ index ];
                return {
                    ...state,
                    quickView: true,
                    articuloDetail: item
                };
            }
            break;

        case CLOSE_QUICKVIEW:
            return { ...state, quickView: false };

        default:
            return state;
    }
};
const persistConfig = {
    keyPrefix: "molla-",
    key: "articulos",
    storage
}

export default persistReducer( persistConfig, articuloReducer );