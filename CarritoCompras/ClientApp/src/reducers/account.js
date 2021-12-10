import * as types from '../constants/action-types'
import { findIndex } from "../utils";

import { persistReducer } from "redux-persist";
import storage from 'redux-persist/lib/storage';

const initialState = {
    // marcas: []
};


const loginReducer = ( state = initialState, action ) => {
    
    switch ( action.type ) {
        case types.LOGIN:
            return {
                ...state,
                login : action.login
                
            };
        case types.LOGOUT:
            return {
                ...state,
                logout : action.logout
                
            };
        case types.REGISTER:
            return {
                ...state,
                register : action.register
                
            };
        case types.VERIFY_EMAIL:
            return {
                ...state,
                verify_email : action.verify_email
                
            };
        case types.FORGOT_PASSWORD:
            return {
                ...state,
                forgot_password : action.forgot_password
                
            };
        case types.VALIDATE_RESET_TOKEN:
            return {
                ...state,
                validate_reset_token : action.validate_reset_token
                
            };
        case types.RESET_PASSWORD:
            return {
                ...state,
                reset_password : action.reset_password
                
            };
        case types.GET_ALL:
            return {
                ...state,
                get_all : action.get_all
                
            };
        case types.GET_BY_ID:
            return {
                ...state,
                get_by_id : action.get_by_id
                
            };
        case types.GET_ADDRESS_BY_ID:
            return {
                ...state,
                get_address_by_id : action.get_address_by_id
                
            };
        case types.CREATE:
            return {
                ...state,
                create : action.create
                
            };
        case types.UPDATE:
            return {
                ...state,
                register : action.register
                
            };
        case types._DELETE:
            return {
                ...state,
                _delete : action._delete
                
            };
        default:
            return state;
    }
};

const persistConfig = {
    keyPrefix: "molla-",
    key: "login",
    storage
}

export default persistReducer( persistConfig, loginReducer );