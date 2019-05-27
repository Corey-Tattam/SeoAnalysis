import React, { Component } from 'react';
import { Nav, Navbar } from 'react-bootstrap';
import { GoHome } from 'react-icons/go';
import { FaSearch } from 'react-icons/fa';
import { LinkContainer } from 'react-router-bootstrap';
import './NavMenu.css';

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor (props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true
    };
  }

  toggleNavbar () {
    this.setState({
      collapsed: !this.state.collapsed
    });
  }

  render () {
    return (
        <Navbar bg="dark" variant="dark" expand="lg" collapseOnSelect fixed="top">
            <Navbar.Brand href="/">
                <strong><FaSearch size="1.5em" style={{ paddingBottom: "5px" }} /></strong> Search Engine Analysis Tool
                </Navbar.Brand>
            <Navbar.Toggle />
            <Navbar.Collapse className="justify-content-end">
                <Nav>
                    <Nav.Link>
                        <LinkContainer to={'/'} exact>
                            <Nav.Item>
                                <GoHome size="1.5em" style={{ paddingBottom: "5px" }} /> Home
                                </Nav.Item>
                        </LinkContainer>
                    </Nav.Link>
                </Nav>
            </Navbar.Collapse>
        </Navbar>
    );
  }
}
