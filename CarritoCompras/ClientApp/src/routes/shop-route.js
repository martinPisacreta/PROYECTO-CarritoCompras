import React from 'react';
import { Route, Switch } from 'react-router-dom';

import Layout from '../components/app';
import Cart from '../components/pages/shop/cart';
import Checkout from '../components/pages/shop/checkout';
import { PrivateRouteUser } from './private_route_user';


export default function ShopRoute() {
    return (
        <Switch>
            <Layout>
                <PrivateRouteUser exact path={ `${process.env.PUBLIC_URL}/shop/cart` } component={ Cart } />
                <PrivateRouteUser exact path={ `${process.env.PUBLIC_URL}/shop/checkout` } component={ Checkout } />
            </Layout>
        </Switch>
    );
}