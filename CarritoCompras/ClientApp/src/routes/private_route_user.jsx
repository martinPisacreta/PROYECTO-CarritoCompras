import React from 'react';
import { Route, Redirect } from 'react-router-dom';

import { usuariosService } from '../api';


// El componente PrivateRoute representa un componente de ruta si el usuario ha iniciado sesión  y tiene un rol autorizado para la ruta,
// si el usuario no ha iniciado sesión, se le redirige a la /login página, 
// si el usuario ha iniciado sesión pero no rol autorizado son redirigidos a la página de inicio.




function PrivateRouteUser({ component: Component, roles, ...rest }) {
    return (
        <Route {...rest} render={props => {
            const user = usuariosService.userValue;
            // si el usuario no esta logueado...
            if (!user) {
                // not logged in so redirect to login page with the return url
                
                return <Redirect to={{ pathname: `${process.env.PUBLIC_URL}/usuario/login`, state: { from: props.location } }} />
            }

            // console.log(Component)
            // // check if route is restricted by role
            // if (roles && roles.indexOf(user.rol) === -1) {
            //     // role not authorized so redirect to home page
            //     return <Redirect to={{ pathname: `${process.env.PUBLIC_URL}/usuario/login`}} />
            // }

            
            // authorized so return component
            return <Component {...props} />
        }} />
    );
}

export { PrivateRouteUser };