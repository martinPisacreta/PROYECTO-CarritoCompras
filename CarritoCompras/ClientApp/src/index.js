import React, { useEffect } from 'react';
import ReactDOM from 'react-dom';
import { Provider } from 'react-redux';
import { BrowserRouter } from 'react-router-dom';
import 'react-app-polyfill/ie11';
import { history } from './components/helpers';
import { PersistGate } from 'redux-persist/integration/react';

// import store
import store, { persistor } from './store';

// import action
//import { getAllArticulos, refreshStore } from './actions'; los metodos de Articulos estan dentro de "articulo-dev-express-list.jsx"
import {  refreshStore } from './actions';

// import routes
import AppRoute from './routes';

// import Utils
import { initFunctions } from './utils';

import LoadingOverlay from './components/features/loading-overlay';

export function Root() {
    initFunctions();
    // localStorage.removeItem('molla-articulos'); //borro los articulos
    // localStorage.removeItem('molla-products'); //borro los products en caso de que existan
    // store.dispatch( getAllArticulos() );
    useEffect( () => {

        if ( store.getState().modal.current !== 13 ) {
            store.dispatch( refreshStore( 13 ) );
        }
      
    }, [] )


    return (
        <Provider store={ store } >
            <PersistGate persistor={ persistor } loading={ <LoadingOverlay /> }>
                <BrowserRouter basename={ '/' } >
                    <AppRoute />
                </BrowserRouter>
            </PersistGate>
        </Provider>
    );
}

ReactDOM.render( <Root />, document.getElementById( 'root' ) );