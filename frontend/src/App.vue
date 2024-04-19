<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router'

</script>

<template>

  <header>
    

    <nav>
        <RouterLink to="/">
          <img class="logo" alt="CanneTV logo" src="@/assets/logo_ctv_rouge_txt_blc.png" width="125"  />
          Competitions
        </RouterLink>
        <RouterLink to="/about">
          About
        </RouterLink>

    </nav>
  </header>

  <RouterView />
</template>

<style scoped>
nav {
  display: flex;
  
}
nav a {
  display: flex;
  align-items: center;
  width: 20%;
  text-decoration: none;
  padding: 20px;
  color: white;
  font-size: 20px;
  letter-spacing: 1px;
  position: relative;
}

nav a::after {
  content: '';
  position: absolute;
  width: 100%;
  transform: scaleX(0);
  height: 2px;
  bottom: 10px;
  left: 0;
  background-color: #fff;
  transform-origin: bottom right;
  transition: transform 0.25s ease-out;
}

nav a:hover::after
{
  transform: scaleX(1);
  transform-origin: bottom left;
}

nav a img.logo {
  margin-right: 20px;
}
</style>
