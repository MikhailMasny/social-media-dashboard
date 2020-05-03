import * as React from "react";
import NavMenu from "../navMenu/";
import Footer from "../footer/"

export default (props: { children?: React.ReactNode }) => (
	<React.Fragment>
		<NavMenu />
		<div>
			{props.children}
		</div>
		<Footer />
	</React.Fragment>
);
