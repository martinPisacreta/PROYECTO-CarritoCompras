import { usuariosService } from '../../api';
import React, { useState, useEffect } from 'react';
import { Route, Redirect } from 'react-router-dom';

export  const fetchWrapper = {
    get,
    post,
    postSinCovertirBodyEnJson,
    put,
    delete: _delete
}





async function get(url) {
    const user = usuariosService.userValue;
    const  requestOptions =
                            user 
                            ?
                                {
                                        method: 'GET',
                                        headers: await authHeader(url) 
                                }
                            :
                                {
                                    method: 'GET'
                                }

    return  fetch(url, requestOptions).then(handleResponse)
}

//ACA LE SACO DOS COSAS YA QUE NO NECESITO TRANFORMAR NADA EN JSON -> 
                        //'Content-Type': 'application/json' -> ya que Indica que el formato del cuerpo de la solicitud es JSON.
                        //JSON.stringify(body) -> ya que transforma el body en json
async function postSinCovertirBodyEnJson(url, body) {

    const user = usuariosService.userValue;
    const requestOptions = 
    user 
        ?
            {
                method: 'POST',
                headers: { ...await authHeader(url) },
                body: body
            }
        :
            {
                method: 'POST',   
                body: body
            }
    return fetch(url, requestOptions).then(handleResponse);
}


async function post(url, body) {

    const user = usuariosService.userValue;
    const requestOptions = 
    user 
        ?
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json', ...await authHeader(url) },
                body: JSON.stringify(body),
            }
        :
            {
                method: 'POST',
                headers: { 'Content-Type': 'application/json'},
                body: JSON.stringify(body),
            }
    return fetch(url, requestOptions).then(handleResponse);
}

async function put(url, body) {

    const requestOptions = {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', ...await authHeader(url) },
        body: JSON.stringify(body)
    };
    
    return fetch(url, requestOptions).then(handleResponse);    
}


// prefixed with underscored because delete is a reserved word in javascript
async function _delete(url) {
    const requestOptions = {
        method: 'DELETE',
        headers: await authHeader(url)
    };
    return fetch(url, requestOptions).then(handleResponse);
}



async function authHeader(url) {
    const user = usuariosService.userValue;
    
    const isLoggedIn = user && user.token;
    const isApiUrl = url.startsWith(process.env.PUBLIC_URL);


    if (isLoggedIn && isApiUrl) {
        return { Authorization: `Bearer ${user.token}` };
    } else {
    
        return {};
    }
}

function handleResponse(response) {
    return response.text()
    .then(text => {
       
        const data = text && JSON.parse(text);
        if (!response.ok) {
            if ([401, 403].includes(response.status)) {

                const error = "Acceso no autorizado" + ' -- ' +  response.status;
                //usuariosService.logout();

                //LA LINEA DE ABAJO SE RELACIONA TAMBIEN CON ...src\routes\private_route_admin.jsx
                window.location = `${process.env.PUBLIC_URL}`; 
                
                return Promise.reject(new Error(error));
               
            }

            const error = (data && data.message) + ' -- ' +  response.status;
            return Promise.reject(new Error(error));
  
        }
        else{
            return data;
        }
       
    });
}