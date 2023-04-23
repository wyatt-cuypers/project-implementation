import React, { Component } from 'react';
import { Collapse, Navbar, NavbarBrand, NavbarToggler, NavItem, NavLink } from 'reactstrap';
import { Link } from 'react-router-dom';
import './NavMenu.css';

export class NavMenu extends Component {
  //static displayName = NavMenu.name;

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

  render() {
    return (
        <nav style={styles.nav}>
          <ul style={styles.ul}>
            <li style={styles.li}><a style={styles.navItem} href="/" class="nav-item">Open PDF</a></li>
            <li style={styles.li}><a style={styles.navItem} href="/pdf-generator" class="nav-item">Generate PDFs</a></li>
          </ul>
        </nav>
    );
  }
}

const styles = {
  nav: {
    backgroundColor: '#00b1f2',
    display: 'flex',
    justifyContent: 'center',
  },
  ul: {
    display: 'flex',
    listStyleType: 'none',
    margin: 0,
    padding: 0,
    width: '100%',
  },
  li: {
    width: '50%',
  },
  navItem: {
    color: '#fff',
    display: 'block',
    padding: '1em',
    textAlign: 'center',
    textDecoration: 'none',
    transition: 'backgroundColor 0.2s ease-in-out',
    fontWeight: 'bold'
  }
}
