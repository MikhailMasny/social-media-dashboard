import * as React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/home/index';
import Counter from './components/Counter';

export default () => (
    <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/counter' component={Counter} />
    </Layout>
);
