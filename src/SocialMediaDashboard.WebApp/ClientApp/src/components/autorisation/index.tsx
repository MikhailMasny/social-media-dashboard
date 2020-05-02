import React, { Component } from 'react'
import { connect } from 'react-redux';
import { ApplicationState } from '../../store';
import { AurorisationState } from './reducer';
import { actionCreators } from './actions'

type AutorisationProps =
    AurorisationState &
    typeof actionCreators;

enum InputTypes {
    Text = "Text",
    Password = "Password"
}

export class Autorisation extends Component<AutorisationProps> {
    constructor(props: Readonly<AutorisationProps>) {
        super(props);

        this.onLoginSubmit = this.onLoginSubmit.bind(this);
    }
    state = {
        login: '',
        password: '',
    }

    render() {
        const { isLoggedIn } = this.props;
        return (
            <React.Fragment>
                {isLoggedIn ?
                    (
                        <>
                            <label>You are logged in!</label>
                            <button onClick={(event) => this.onLogoutSubmit(event)}>Logout</button>
                        </>
                    )
                    :
                    (
                        <>
                            <label>Enter login:</label>
                            <input type={InputTypes.Text} onInput={(event) => this.onInput(InputTypes.Text, event)}></input>
                            <label>Enter password:</label>
                            <input type={InputTypes.Password} onInput={(event) => this.onInput(InputTypes.Password, event)}></input>
                            <button onClick={(event) => this.onLoginSubmit(event)}>Submit</button>
                        </>
                    )
                }

            </React.Fragment>
        )
    }
    //TODO: Replace to event handler
    onLoginSubmit(event: React.MouseEvent) {
        const {
            login,
            password
        } = this.state;
        this.props.onLogin(login, password);
    }

    onLogoutSubmit(event: React.MouseEvent) {
        this.props.onLogOut();
    }

    onInput(type: InputTypes, event: React.FormEvent<HTMLInputElement>) {
        const newValue = (event.target as HTMLTextAreaElement).value;
        switch (type) {
            case InputTypes.Text: {
                this.setState({
                    login: newValue
                })
                break;
            }
            case InputTypes.Password: {
                this.setState({
                    password: newValue
                })
                break;
            }
        }
    }
}

export default connect(
    (state: ApplicationState) => state.autorisation,
    actionCreators
)(Autorisation);