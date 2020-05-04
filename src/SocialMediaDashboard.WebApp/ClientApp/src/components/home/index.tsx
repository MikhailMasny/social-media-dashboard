import * as React from "react";
import { connect } from "react-redux";
import Autorisation from "../autorisation/index";
import About from "./about";

import "./styles/home.scss";

const Home = () => (
	<div>
		<div className="aboutContainer">
			<About />
		</div>
		<div className="auth">
			<Autorisation />
		</div>
	</div>
);

export default connect()(Home);
