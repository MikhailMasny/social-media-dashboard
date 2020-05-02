import * as React from 'react';
import { connect } from 'react-redux';
import Autorisation from '../autorisation/index'

const Home = () => (
    <div>
        <Autorisation ></Autorisation>
    </div>
);

export default connect()(Home);
