<template>
    <div class="container mt-5">
      <h1 class="text-center">Máquina Expendedora de Café</h1>
      <div class="row mt-4">
        <div class="col-md-6">
          <h3>Tipos de Café</h3>
          <ul class="list-group">
            <li v-for="(coffee, index) in coffees" :key="index" class="list-group-item d-flex justify-content-between align-items-center">
              {{ coffee.name }} - {{ coffee.price }} colones
              <span class="badge badge-primary badge-pill">{{ coffee.quantity }}</span>
            </li>
          </ul>
        </div>
        <div class="col-md-6">
          <h3>Seleccionar Café</h3>
          <div class="form-group">
            <label for="coffeeType">Tipo de Café</label>
            <select v-model="selectedCoffee" class="form-control" id="coffeeType">
              <option v-for="(coffee, index) in coffees" :key="index" :value="index">{{ coffee.name }}</option>
            </select>
          </div>
          <div class="form-group">
            <label for="coffeeQuantity">Cantidad</label>
            <input type="text" v-model.number="quantity" class="form-control" id="coffeeQuantity" @keypress="isNumber" @input="clearError">
          </div>
          <button @click="addCoffee" class="btn btn-primary">Agregar</button>
          <h4 class="mt-4">Total: {{ total }} colones</h4>
          <div v-if="error" class="alert alert-danger mt-2">{{ error }}</div>
        </div>
      </div>
      <div class="row mt-4">
        <div class="col-md-6">
          <h3>Ingresar Dinero</h3>
          <div class="form-group">
            <label for="money">Monto</label>
            <input type="text" v-model.number="money" class="form-control" id="money" @keypress="isNumber" @input="clearError">
          </div>
          <button @click="makePurchase" class="btn btn-success">Comprar</button>
          <div v-if="message" class="alert alert-info mt-2">{{ message }}</div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from 'axios';
  
  export default {
    data() {
      return {
        coffees: [
          { name: 'Americano', price: 950, quantity: 10 },
          { name: 'Capuchino', price: 1200, quantity: 8 },
          { name: 'Latte', price: 1350, quantity: 10 },
          { name: 'Mocachino', price: 1500, quantity: 15 }
        ],
        selectedCoffee: 0,
        quantity: 1,
        total: 0,
        money: 0,
        error: '',
        message: ''
      };
    },
    methods: {
      addCoffee() {
        const coffee = this.coffees[this.selectedCoffee];
        if (this.quantity > coffee.quantity) {
          this.error = 'Cantidad insuficiente de café';
          return;
        }
        this.total += coffee.price * this.quantity;
        coffee.quantity -= this.quantity;
        this.error = '';
      },
      makePurchase() {
        axios.post('/api/compra', {
          total: this.total,
          money: this.money
        })
        .then(response => {
          this.message = response.data.message;
          this.total = 0;
          this.money = 0;
        })
        .catch(error => {
          this.message = 'Fallo al realizar la compra';
        });
      },
      isNumber(event) {
        const charCode = event.which ? event.which : event.keyCode;
        const keyIsNotDigit = charCode < '0'.charCodeAt(0) || charCode > '9'.charCodeAt(0);
        if (keyIsNotDigit) {
          event.preventDefault();
        }
      },
      clearError() {
        this.error = '';
      }
    }
  };
  </script>
  
  <style>
  .container {
    max-width: 800px;
  }
  </style>